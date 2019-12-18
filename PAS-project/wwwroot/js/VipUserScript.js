function ShowHideDiv() {
    var standardRadio = document.getElementById("chkYes");
    var vipRadio = document.getElementById("chkNo");
    var dvString = document.getElementById("dvString");
    dvString.style.display = standardRadio.checked ? "block" : "none";
}