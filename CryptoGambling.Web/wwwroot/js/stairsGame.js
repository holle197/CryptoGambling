var currRow = 1;
var betAmount = 0;
/*
$(".gameField").on("click", function () {
    $(this).attr("row");
    $('[row="1"]').addClass("currRow");
});*/

$(document.body).on("click", ".start", function () {
    $(this).addClass("end");
    $(this).removeClass("start");
    $("#playEndText").html("END");
    $('[row="1"]').addClass("currRow");
    betAmount = parseFloat($("#amountInp").val());
    PlayEndSound();
});

$(document.body).on("click", ".end", function () {
    $(this).addClass("start");
    $(this).removeClass("end");
    $("#playEndText").html("PLAY");

    $(".currRow").removeClass("currRow");
    currRow = 1;
    PlayEndSound();

});

$(document.body).on("click", ".currRow", function () {
    if ($(".currRow").hasClass("clicked")) {
        return false;
    }
    $(this).addClass("clicked");
    var field = $(this).attr("col");
    $.ajax({
        type: "post",
        url: "https://localhost:7223/stairs/bet",
        data: { betAmount: betAmount, difficulty: difficulty, currency: currency, intLuckyField: field },
        success: function (result) {
            console.log(result)
            if (result["errorMessage"]) {
                //later dispaly error msg to the user
                return;
            }
            if (result["isGameWinning"] == true) {
                console.log(121112);
                console.log(result)
            }
            console.log(result)
        }
    });
});