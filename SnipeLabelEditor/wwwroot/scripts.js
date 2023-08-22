function PrintPNGBase64(source)
{
    printJS({
        printable: source,
        type: 'png',
        base64: true,
    }); 
}

function PrintPDFBase64(source)
{
    printJS({
        printable: source,
        type: 'pdf',
        base64: true,
    });
}

function GetCurrentURL(iframeid){
    const iframe = document.getElementById(iframeid);
    return  iframe.contentWindow.location;
}