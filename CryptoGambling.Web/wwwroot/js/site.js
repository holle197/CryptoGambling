// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



function checkBtcDeposite() {
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
            }
        }
    });
}



