// // Função para atualizar o cronômetro da sessão
function updateSessionTimer(sessionEndTime) {
    // Verifica se o tempo de término da sessão foi recuperado corretamente
    if (sessionEndTime.trim() !== "") {
        // Converte a string do tempo de término da sessão em um objeto Date
        var sessionEnd = new Date(sessionEndTime);
        console.log("Tempo de término da sessão recuperado:", sessionEnd);
        if (!isNaN(sessionEnd.getTime())) {
            // Se o tempo de término da sessão for válido, inicia o cronômetro
            var intervalId = setInterval(function() {
                var now = new Date().getTime();
                var distance = sessionEnd - now;
                // Verifica se o tempo restante é negativo e, se for, para o intervalo
                if (distance < 0) {
                    clearInterval(intervalId);
                    console.log("Sessão expirada.");
                    // Redireciona para a página de logout usando um URL absoluto
                    window.location.href = '/Login/Logout';
                    return;
                }
                var hours = Math.floor(distance / (1000 * 60 * 60));
                var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
                var seconds = Math.floor((distance % (1000 * 60)) / 1000);
                // Formata os valores de horas, minutos e segundos
                var formattedHours = ("0" + hours).slice(-2);
                var formattedMinutes = ("0" + minutes).slice(-2);
                var formattedSeconds = ("0" + seconds).slice(-2);
                // Atualiza o elemento HTML com o tempo restante da sessão no formato HH:mm:ss
                var sessionTimerElement = document.getElementById("sessionTimer");
                if (sessionTimerElement) {
                    sessionTimerElement.innerText = formattedHours + ":" + formattedMinutes + ":" + formattedSeconds;
                } else {
                    console.error("Elemento HTML sessionTimer não encontrado.");
                }
            }, 1000);
        } else {
            // Se o tempo de término da sessão for inválido, registra um erro no console
            console.error("Tempo de término da sessão inválido.");
        }
    } else {
        // Se o tempo de término da sessão não foi encontrado na sessão, registra um erro no console
        console.error("Tempo de término da sessão não encontrado.");
    }
}

// Função para esperar o carregamento completo da página antes de executar o código JavaScript
document.addEventListener("DOMContentLoaded", function() {
    // Recupera o tempo de término da sessão do atributo data do elemento HTML
    var sessionEndTime = document.getElementById("sessionTimeoutData").getAttribute("data-session-timeout");
    console.log("Tempo de término da sessão recuperado:", sessionEndTime); // Adiciona um log para verificar o valor recuperado
    // Chama a função para atualizar o cronômetro da sessão
    updateSessionTimer(sessionEndTime);
});