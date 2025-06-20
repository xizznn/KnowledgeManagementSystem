document.addEventListener("DOMContentLoaded", () => {
    const token = localStorage.getItem("token");

    if (token) {
        const payload = JSON.parse(atob(token.split('.')[1]));

        const email = payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"];
        const role = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

        console.log("Decoded JWT payload:", payload);

        if (email) {
            document.getElementById("userEmailDisplay").innerHTML =
                `<span class="nav-link text-white">${email}</span>`;
        }

        // Скрываем ссылку "Войти"
        document.getElementById("loginLink").style.display = "none";

        // Скрываем ссылку "Регистрация"
        const registerLink = document.querySelector('a[href="/Register"]');
        if (registerLink) {
            registerLink.style.display = "none";
        }

        // Показываем ссылку "Выйти"
        document.getElementById("logoutLink").style.display = "block";

        // Показываем ссылку "Мой профиль"
        const profileNavItem = document.getElementById("profileNavItem");
        if (profileNavItem) {
            profileNavItem.classList.remove("d-none");
        }

        // Если роль Admin — показываем админ-панель
        if (role === "Admin") {
            document.getElementById("adminPanelNav").classList.remove("d-none");
        }
    }

    const logout = document.getElementById("logoutLink");
    if (logout) {
        logout.addEventListener("click", () => {
            localStorage.removeItem("token");
            window.location.href = "/Login";
        });
    }
});
