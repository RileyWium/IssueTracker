function copyToClipboard(copyID) {
    var copyText = document.getElementById(copyID);

    copyText.select();
    copyText.setSelectionRange(0, 99999);
    
    document.execCommand("copy");

    alert("Copied the text: " + copyText.value);
}