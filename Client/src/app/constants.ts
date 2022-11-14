export const Server_URL = "https://localhost:5000"

export function serviceUrlCombiner(controller: string, method: string = "") {
    if (method != "")
        return `${Server_URL}/${controller}/${method}`
    else
        return `${Server_URL}/${controller}`
}
declare global {
    interface Date {
        newUTCDate () : Date
    }
}

Date.prototype.newUTCDate = function() {
    const date = new Date();
    const utc = new Date(date.getTime() + date.getTimezoneOffset() * 60000);
    return new Date(utc);
}