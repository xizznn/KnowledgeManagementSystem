const apiUrl = ""; // если API на том же хосте

function getAuthHeaders() {
    const token = localStorage.getItem("token");
    return token
        ? { Authorization: `Bearer ${token}`, "Content-Type": "application/json" }
        : { "Content-Type": "application/json" };
}

async function apiGet(endpoint) {
    const res = await fetch(apiUrl + endpoint, {
        headers: getAuthHeaders()
    });
    if (!res.ok) {
        const error = await res.json().catch(() => null);
        throw new Error(error?.message || `Ошибка запроса GET ${res.status}`);
    }
    return await res.json();
}

async function apiPost(endpoint, body = {}) {
    const res = await fetch(apiUrl + endpoint, {
        method: "POST",
        headers: getAuthHeaders(),
        body: JSON.stringify(body)
    });
    if (!res.ok) {
        const error = await res.json().catch(() => null);
        throw new Error(error?.message || `Ошибка запроса POST ${res.status}`);
    }
    // Можно возвращать данные, если API возвращает
    try {
        return await res.json();
    } catch {
        return true; // Если нет тела ответа, возвращаем true
    }
}

async function apiPut(endpoint, body = {}) {
    const res = await fetch(apiUrl + endpoint, {
        method: "PUT",
        headers: getAuthHeaders(),
        body: JSON.stringify(body)
    });
    if (!res.ok) {
        const error = await res.json().catch(() => null);
        throw new Error(error?.message || `Ошибка запроса PUT ${res.status}`);
    }
    try {
        return await res.json();
    } catch {
        return true;
    }
}

async function apiDelete(endpoint) {
    const res = await fetch(apiUrl + endpoint, {
        method: "DELETE",
        headers: getAuthHeaders()
    });
    if (!res.ok) {
        const error = await res.json().catch(() => null);
        throw new Error(error?.message || `Ошибка запроса DELETE ${res.status}`);
    }
    return true;
}

// Примеры функций для работы с избранным тестами — можно оставить или убрать
async function isTestFavorite(testId) {
    return await apiGet(`/api/FavoritesTests/check/${testId}`);
}

async function addTestToFavorites(testId) {
    return await apiPost(`/api/FavoritesTests/${testId}`, {});
}

async function removeTestFromFavorites(testId) {
    return await apiDelete(`/api/FavoritesTests/${testId}`);
}

async function getFavoriteTests() {
    return await apiGet("/api/FavoritesTests");
}
