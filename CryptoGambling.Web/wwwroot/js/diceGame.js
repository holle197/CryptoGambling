var betAmount = 0;
var diff = "EASY";
var luckyNumber = 25;


$("#volume").on("click", function () {
    var val = parseInt($(this).val());
    $("#diff").html(val);
    luckyNumber = val;
    switch (val) {

        case 25: diff = "EASY";
            $("#winChance").html(1.24);
            difficulty = 0;
            break;
        case 50: diff = "MEDIUM";
            $("#winChance").html(1.98);
            difficulty = 1;
            break;
        case 75: diff = "HARD";
            $("#winChance").html(3.96);
            difficulty = 2;
            break;
    }
});




$(document.body).on("click", ".start", function () {
    $(this).addClass("end");
    $(this).removeClass("start");
    let bam = parseFloat($("#amountInp").val());
    let walbal = parseFloat($("#walletBalance").html());
    var differ = walbal - bam;
    if (differ >= 0) {

        betAmount = bam;
        $.ajax({
            type: "post",
            url: "/dice/bet",
            data: { betAmount: betAmount, difficulty: difficulty, currency: currency, intLuckyField: luckyNumber },
            success: function (result) {
                $(".end").addClass("start");
                $(".end").removeClass("end");
                if (result["errorMessage"]) {

                    $("#errorMsg").html(result["errorMessage"]);
                    setTimeout(function () { $("#errorMsg").html("") }, 5000);
                    return;
                }
                if (result["isGameWinning"]) {
                    $("#diceResult").addClass("green");
                    $("#diceResult").removeClass("red");
                    $("#diceResult").html(showNumberOnWin(luckyNumber));

                    $("#winOrLose").html("+" + result["profit"].toFixed(8));
                    $("#winOrLose").addClass("green");
                    $("#winOrLose").removeClass("red");
                    let finalBalance = parseFloat($("#walletBalance").html()) + result["profit"];
                    $("#walletBalance").html(finalBalance.toFixed(8));

                    WinSound();
                    return
                }
                //on lose
                $("#diceResult").addClass("red");
                $("#diceResult").removeClass("green");
                $("#diceResult").html(showNumberOnLose(luckyNumber));

                $("#winOrLose").html(result["profit"].toFixed(8));
                $("#winOrLose").addClass("red");
                $("#winOrLose").removeClass("green");
                let finalBalance = parseFloat($("#walletBalance").html()) - betAmount;
                $("#walletBalance").html(finalBalance.toFixed(8));

                LoseSound();

            }
        });

    } else {
        $("#errorMsg").html("Insufficient Balance.");
        setTimeout(function () { $("#errorMsg").html("") }, 5000);
        $(".end").addClass("start");
        $(".end").removeClass("end");
        return;
    }
});

function showNumberOnLose(x) {
    if (x == 25) {
        return Math.floor(Math.random() * 26);
    } else if (x == 50) {
        return Math.floor(Math.random() * 51);
    } else {
        return Math.floor(Math.random() * 71);
    }
}
function showNumberOnWin(x) {
    if (x == 25) {
        return Math.floor(Math.random() * (100 - 26 + 1)) + 26;
    } else if (x == 50) {
        return Math.floor(Math.random() * (100 - 51 + 1)) + 51;

    } else {
        return Math.floor(Math.random() * (100 - 76 + 1)) + 76;

    }
}