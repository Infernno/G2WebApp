var messageTimeOut = 4000;

function showMessage(message, type) {
    noty(
        {
            text: message,
            layout: "bottomRight",
            type: type,
            theme: "bootstrapTheme",
            timeout: messageTimeOut
        }
    );
}
