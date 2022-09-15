// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

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



function DepositeSound() {
    var sound = new Audio("../sounds/depositesound.wav");
    sound.loop = false;
    sound.play();
}

function GetBtcAddress() {
    $.ajax({
        type: "get",
        url: "https://localhost:7223/wallet/GetBtcDepositeAddress",

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
        url: "https://localhost:7223/wallet/GetLtcDepositeAddress",

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
        url: "https://localhost:7223/wallet/GetDogeDepositeAddress",

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
        url: "https://localhost:7223/wallet/CheckBtcDeposite",

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
        url: "https://localhost:7223/wallet/CheckLtcDeposite",

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
        url: "https://localhost:7223/wallet/CheckDogeDeposite",

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
