/*
    Animação JQuery utilizando os eventos "mouseenter" e "mouseleave" para gerar o efeito do ícone de porta abrindo e fechando 
*/

$(document).ready(function(){
    $("#close-link").mouseenter(function(){
        $(this).find('i').removeClass('bi-door-closed-fill').addClass('bi-door-open-fill');
    }).mouseleave(function(){
        $(this).find('i').removeClass('bi-door-open-fill').addClass('bi-door-closed-fill');
    });
});