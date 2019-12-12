function ShowHideDiv() {
    var chkYes = document.getElementById("chkYes");
    var dvString = document.getElementById("dvString");
    dvString.style.display = chkYes.checked ? "block" : "none";
}