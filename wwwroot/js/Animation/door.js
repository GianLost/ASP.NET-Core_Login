import { iconLogin, iconLogout} from "../Constants/constantsComponent.js";

/**
 * "function openCloseDoor()" é a função responsável por criar a animação do ícone de porta abrindo e fechando, a função utiliza os eventos "mouseenter" e "mouseleave" para criar o efeito da animação;
 * 
 * @param {jQuery} icon Elemento jQuery que representa o ícone para a animação;
 */

function openCloseDoor(icon) {
    $(icon).mouseenter(function(){
        $(this).find('i').removeClass('bi-door-closed-fill').addClass('bi-door-open-fill');
    }).mouseleave(function(){
        $(this).find('i').removeClass('bi-door-open-fill').addClass('bi-door-closed-fill');
    });
}

$(document).ready(function(){
    openCloseDoor(iconLogin);
    openCloseDoor(iconLogout);
});