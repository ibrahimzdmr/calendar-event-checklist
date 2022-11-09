export const Server_URL = "https://localhost:5000"

export function serviceUrlCombiner(controller: string, method: string = "") {
    if (method != "")
        return `${Server_URL}/${controller}/${method}`
    else
        return `${Server_URL}/${controller}`
}