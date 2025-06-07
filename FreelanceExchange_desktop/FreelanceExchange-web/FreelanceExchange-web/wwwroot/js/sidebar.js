document.addEventListener('DOMContentLoaded', function() {
    const sidebar = document.getElementById("mySidebar");
    const openBtn = document.getElementById("openbtn");
    const closeBtn = document.getElementById("closebtn");
    const main = document.getElementById("main");

    function openNav() {
        sidebar.classList.add("open");
        main.style.marginLeft = "250px";
    }

    function closeNav() {
        sidebar.classList.remove("open");
        main.style.marginLeft = "0";
    }

    openBtn.addEventListener("click", openNav);
    closeBtn.addEventListener("click", closeNav);

    // Закрываем панель при клике вне её
    document.addEventListener("click", function(event) {
        if (!sidebar.contains(event.target) && !openBtn.contains(event.target) && sidebar.classList.contains("open")) {
            closeNav();
        }
    });
}); 