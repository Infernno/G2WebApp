function verifyForm(id, url, onSuccess, onFailure, reloadOnTimeOut) {

    if (typeof reloadOnTimeOut === "undefined")
        reloadOnTimeOut = false;

    $(document).ready(function () {
        $(id).unbind("submit");

        $(id)
            .attr("action", url)
            .bind("submit", function () {
                var form = this;
                if ($(id).valid()) {
                    $.post($(form).attr("action"), $(form).serialize(), function (data) {
                        if (data.error) {
                            showMessage(data.error, "error");
                            if (onFailure != null) onFailure(data.error);
                        }
                        else {
                            if (onSuccess != null) onSuccess(data);

                            function reload() {
                                if (data.redirect == null) location.reload(true);
                                else document.location = data.redirect;
                            }

                            if (reloadOnTimeOut) {
                                setTimeout(reload, messageTimeOut);
                            } else {
                                reload();
                            }

                        }
                    });
                }

                return false;
            });
    });
}