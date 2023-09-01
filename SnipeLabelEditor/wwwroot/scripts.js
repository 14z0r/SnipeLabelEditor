function PrintPNGBase64(source)
{
    printJS({
        printable: source,
        type: 'image',
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

window.downloadFileFromStream = async (fileName, contentStreamReference) => {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
    URL.revokeObjectURL(url);
}