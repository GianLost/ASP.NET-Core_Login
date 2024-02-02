import { loginPasswordInput, loginTogglePasswordButton, loginPasswordIcon } from "./constantsComponent.js";

/**
 * "function togglePasswordVisibility()" é a função responsável por alterar a visibilidade dos campos de senha nos formulários em que é aplicada.
 * - Utiliza os eventos "mousedown" e "mouseup" para exibir e ocultar os caractéres conforme o ícone de exibição é pressionado e/ou solto;
 * 
 * @param {jQuery} passwordInput Elemento jQuery que representa o campo de senha;
 * @param {jQuery} toggleButton Elemento jQuery que representa o botão de alternância de visibilidade da senha;
 * @param {jQuery} passwordIcon Elemento jQuery que representa o ícone que se altera de acordo com a visibidade da senha;
 */

function togglePasswordVisibility(passwordInput, toggleButton, passwordIcon) {
    let isMouseDown = false;

    toggleButton.on("mousedown mouseup mouseleave", function (event) {
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
}

/**
 * Função que é executada quando o documento HTML foi completamente carregado e está pronto para manipulação.
 * Neste momento, a visibilidade da senha é ativada, permitindo ao usuário alternar entre exibir e ocultar a senha.
 * Isso é feito chamando a função `togglePasswordVisibility` com os elementos relevantes do formulário.
 */

$(document).ready(function () {
    // Ativa a funcionalidade de alternância de visibilidade da senha
    togglePasswordVisibility(loginPasswordInput, loginTogglePasswordButton, loginPasswordIcon);
});
