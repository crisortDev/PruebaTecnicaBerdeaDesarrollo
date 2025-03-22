// scripts.js
document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");
    form.addEventListener("submit", function (event) {
        const input = document.querySelector("input[name='pokemonName']");
        if (input.value.trim() === "") {
            alert("Por favor, ingresa un nombre de Pokémon.");
            event.preventDefault(); // Evita que el formulario se envíe
        }
    });
});