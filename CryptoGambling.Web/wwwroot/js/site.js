// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//wallet section
var btcAddress;
var ltcAddress;
var dogeAddress;

var btcNetwork = "btctest";
var dogeNetwork = "dogetest";
var ltcNetwork = "ltctest";

var minNetConf = 0;

var minBtcDeposite = 0.0001;
var minDogeDeposite = 10;
var minLtcDeposite = 0.001;


//game section
var currency = 0;
var difficulty = 0;

//bet section
var btcBetAmount = 0.00001;
var ltcBetAmount = 0.00001;
var dogeBetAmount = 1;

var btcMinBet = 0.00001;
var ltcMinBet = 0.00001;
var dogeMinBet = 1;


function DepositeSound() {
    var sound = new Audio("../sounds/depositesound.wav");
    sound.loop = false;
    sound.play();
}
function WinSound() {
    var sound = new Audio("../sounds/winsound.wav");
    sound.loop = false;
    sound.play();
}
function LoseSound() {
    var sound = new Audio("../sounds/losesound.wav");
    sound.loop = false;
    sound.play();
}
function PlayEndSound() {
    var sound = new Audio("../sounds/startendgame.wav");
    sound.loop = false;
    sound.play();
}

function GetBtcAddress() {
    $.ajax({
        type: "get",
        url: "/Wallet/GetBtcDepositeAddress",

        success: function (address) {
            if (address) {
                btcAddress = address;
            }
        }
    });
}
function GetLtcAddress() {
    $.ajax({
        type: "get",
        url: "/Wallet/GetLtcDepositeAddress",

        success: function (address) {
            if (address) {
                ltcAddress = address;
            }
        }
    });
}
function GetDogeAddress() {
    $.ajax({
        type: "get",
        url: "/Wallet/GetDogeDepositeAddress",

        success: function (address) {
            if (address) {
                dogeAddress = address;
            }
        }
    });
}

function InternalCheckBtcDeposite() {
    $.ajax({
        type: "get",
        url: "/Wallet/CheckBtcDeposite",

        success: function (deposite) {
            //deposite is not null
            if ($.trim(deposite)) {
                let currBalance = parseFloat($("#btcBalance").html());
                let depositedBalance = parseFloat(deposite.amount);
                let total = Number(currBalance + depositedBalance).toFixed(8);

                $("#btcBalance").text(total);
                DepositeSound();
            }
        }
    });
}
function InternalCheckLtcDeposite() {
    $.ajax({
        type: "get",
        url: "/Wallet/CheckLtcDeposite",

        success: function (deposite) {
            //deposite is not null
            if ($.trim(deposite)) {
                let currBalance = parseFloat($("#ltcBalance").html());
                let depositedBalance = parseFloat(deposite.amount);
                let total = Number(currBalance + depositedBalance).toFixed(8);

                $("#ltcBalance").text(total);
                DepositeSound();
            }
        }
    });
}

function InternalCheckDogeDeposite() {
    $.ajax({
        type: "get",
        url: "/Wallet/CheckDogeDeposite",

        success: function (deposite) {
            //deposite is not null
            if ($.trim(deposite)) {
                let currBalance = parseFloat($("#dogeBalance").html());
                let depositedBalance = parseFloat(deposite.amount);
                let total = Number(currBalance + depositedBalance).toFixed(8);

                $("#dogeBalance").text(total);
                DepositeSound();
            }
        }
    });
}

function ExternalCheckBtcDeposite() {
    if (btcAddress) {
        $.ajax({
            type: "get",
            url: `https://www.sochain.com/api/v2/get_address_balance/${btcNetwork}/${btcAddress}/${minNetConf}`,

            success: function (response) {
                //deposite is not null
                if (response.status == "success") {
                    let balance = parseFloat(response.data.confirmed_balance);
                    if (balance >= minBtcDeposite) {

                        InternalCheckBtcDeposite();

                    }
                }
            }
        });
    } else {
        GetBtcAddress();
    }
}

function ExternalCheckLtcDeposite() {
    if (btcAddress) {
        $.ajax({
            type: "get",
            url: `https://www.sochain.com/api/v2/get_address_balance/${ltcNetwork}/${ltcAddress}/${minNetConf}`,

            success: function (response) {
                //deposite is not null
                if (response.status == "success") {
                    let balance = parseFloat(response.data.confirmed_balance);
                    if (balance >= minLtcDeposite) {

                        InternalCheckLtcDeposite();

                    }
                }
            }
        });
    } else {
        GetLtcAddress();
    }
}
function ExternalCheckDogeDeposite() {
    if (btcAddress) {
        $.ajax({
            type: "get",
            url: `https://www.sochain.com/api/v2/get_address_balance/${dogeNetwork}/${dogeAddress}/${minNetConf}`,

            success: function (response) {
                //deposite is not null
                if (response.status == "success") {
                    let balance = parseFloat(response.data.confirmed_balance);
                    if (balance >= minDogeDeposite) {
                        InternalCheckDogeDeposite();
                    }
                }
            }
        });
    } else {
        GetDogeAddress();
    }
}




GetBtcAddress();
GetLtcAddress();
GetDogeAddress();



//switch currency

$("#btcDropdown").on("click", function () {
    currency = 0;
    let currBalance = $("#btcBalance").html();
    $("#walletImg").attr("src", "../img/btcwalleticon.png");
    $("#walletShortName").text("BTC");
    $("#walletBalance").text(currBalance);
    $("#amountInput").text(currBalance);
    $("#amountInp").attr("value", btcMinBet);

});

$("#ltcDropdown").on("click", function () {
    currency = 1;
    let currBalance = $("#ltcBalance").html();
    $("#walletImg").attr("src", "../img/ltcwalleticon.png");
    $("#walletShortName").text("LTC");
    $("#walletBalance").text(currBalance);
    $("#amountInp").attr("value", ltcMinBet);

});
$("#dogeDropdown").on("click", function () {
    currency = 2;
    let currBalance = $("#dogeBalance").html();
    $("#walletImg").attr("src", "../img/dogewalleticon.png");
    $("#walletShortName").text("DOGE");
    $("#walletBalance").text(currBalance);
    $("#amountInp").attr("value", dogeMinBet);

});


//switch difficulty

$("#dropdownEasy").on("click", function () {
    difficulty = 0;

    $("#currDiffImg").attr("src", "../img/easy.png");
});

$("#dropdownMedium").on("click", function () {
    difficulty = 1;

    $("#currDiffImg").attr("src", "../img/medium.png");
});
$("#dropdownHard").on("click", function () {
    difficulty = 2;

    $("#currDiffImg").attr("src", "../img/hard.png");
});


//amount input validation

function isNumberKey(evt, element) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && !(charCode == 46 || charCode == 8))
        return false;
    else {
        var len = $(element).val().length;
        var index = $(element).val().indexOf('.');
        if (index > 0 && charCode == 46) {
            return false;
        }
        if (index > 0) {
            var CharAfterdot = (len + 1) - index;
            if (CharAfterdot > 8) {
                return false;
            }
        }

    }
    return true;
}


