var betAmount = 0;
var fields = [];

$(document.body).on("click", ".gameField", function () {
    let $this = $(this);
    if (fields.length < 5 && !$this.hasClass("selected")) {
        $this.addClass("selected");
        fields.push(parseInt($this.attr("field")));
    }
});

$(document.body).on("click", ".selected", function () {
    let fld = $(this).attr("field");
    $(this).removeClass("selected");

    var index = fields.indexOf(fld);
    fields.splice(index, 1);

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
            url: "/mines/bet",
            data: { betAmount: betAmount, difficulty: difficulty, currency: currency, listIntLuckyField: fields },
            success: function (result) {
                console.log(result);
                $(".end").addClass("start");
                $(".end").removeClass("end");
                if (result["errorMessage"]) {
                    console.log(result["errorMessage"]);
                    $("#errorMsg").html(result["errorMessage"]);
                    setTimeout(function () { $("#errorMsg").html("") }, 5000);
                    return;
                }
                if (result["isGameWinning"]) {
                    $("#diceResult").addClass("green");
                    $("#diceResult").removeClass("red");

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