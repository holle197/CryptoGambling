var currRow = 1;
var betAmount = 0;
/*
$(".gameField").on("click", function () {
    $(this).attr("row");
    $('[row="1"]').addClass("currRow");
});*/

$(document.body).on("click", ".start", function () {

    let bam = parseFloat($("#amountInp").val());
    let walbal = parseFloat($("#walletBalance").html());
    var diff = walbal - bam;
    if (diff >= 0) {
        $(this).addClass("end");
        $(this).removeClass("start");
        $(".gameField").removeClass("winField");
        $(".gameField").removeClass("loseField");
        $("#playEndText").html("END");
        $('[row="1"]').addClass("currRow");
        betAmount = bam;
        $("#walletBalance").html(diff.toFixed(8))
        PlayEndSound();
    } else {
        $("#errorMsg").html("Insufficient Balance.");
        setTimeout(function () { $("#errorMsg").html("") }, 5000);
    }

});

$(document.body).on("click", ".end", function () {
    $(this).addClass("start");
    $(".gameField").removeClass("clicked");
    $(this).removeClass("end");
    $("#playEndText").html("PLAY");

    $(".currRow").removeClass("currRow");
    currRow = 1;
    let finalBalance = parseFloat($("#walletBalance").html()) + betAmount;
    $("#walletBalance").html(finalBalance.toFixed(8));
    PlayEndSound();

});

$(document.body).on("click", ".currRow", function () {
    if ($(".currRow").hasClass("clicked")) {
        return false;
    }
    $(".currRow").addClass("clicked");
    var field = $(this).attr("col");
    var winOrLoseField = $(this);
    $.ajax({
        type: "post",
        url: "/stairs/bet",
        data: { betAmount: betAmount, difficulty: difficulty, currency: currency, intLuckyField: field },
        success: function (result) {
            if (result["errorMessage"]) {
                $(".end").addClass("start");
                $(".gameField").removeClass("clicked");
                $(".end").removeClass("end");
                $("#playEndText").html("PLAY");

                $(".currRow").removeClass("currRow");
                currRow = 1;
                $("#errorMsg").html(result["errorMessage"]);
                setTimeout(function () { $("#errorMsg").html("") }, 5000);

                return;
            }
            else if (result["isGameWinning"] == true) {
                $(".currRow").removeClass("currRow");
                if (currRow < 6) {
                    currRow++;
                    $(`[row="${currRow}"]`).addClass("currRow");
                    betAmount += parseFloat(parseFloat(result["profit"]).toFixed(8));
                    $("#playEndText").html("TAKE " + betAmount);
                    winOrLoseField.addClass("winField");
                    WinSound();
                    return
                }
                //row is reach max level
                currRow++;
                $(`[row="${currRow}"]`).addClass("currRow");
                betAmount += parseFloat(parseFloat(result["profit"]).toFixed(8));
                winOrLoseField.addClass("winField");
                WinSound();

                $(".end").addClass("start");
                $(".gameField").removeClass("clicked");
                $("end").removeClass("end");
                $("#playEndText").html("PLAY");
                $(".end").addClass("start");
                $(".end").removeClass("end");
                $(".currRow").removeClass("currRow");

                let finalBalance = parseFloat($("#walletBalance").html()) + betAmount;
                $("#walletBalance").html(finalBalance.toFixed(8));
                currRow = 1;
                return;
            }
            //on lose
            winOrLoseField.addClass("loseField");
            $(".end").addClass("start");
            $(".end").removeClass("end");
            $("#playEndText").html("PLAY");
            $(".gameField").removeClass("clicked");

            $(".currRow").removeClass("currRow");
            currRow = 1;
            LoseSound();

        }
    });
});