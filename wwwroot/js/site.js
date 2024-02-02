$(document).ready(function () {
    const passwordInput = $("#login-password");
    const togglePasswordButton = $("#toggle-password");
    const passwordIcon = $("#password-icon");

    let isMouseDown = false;

    togglePasswordButton.on("mousedown mouseup mouseleave", function (event) {
        if (event.type === "mousedown" || event.type === "mouseup") {
            isMouseDown = !isMouseDown;
            const type = isMouseDown ? "text" : "password";
            passwordInput.attr("type", type);
            passwordIcon.toggleClass("bi-eye-fill bi-eye-slash-fill");
        } else if (event.type === "mouseleave" && isMouseDown && passwordInput.attr("type") === "text") {
            passwordInput.attr("type", "password");
            passwordIcon.removeClass("bi-eye-fill").addClass("bi-eye-slash-fill");
        }
    });
});