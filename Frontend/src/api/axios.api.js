import axios from "axios";
import { getTokenFromLocalStorage } from "../helpers/helpers";

const URL = ''

export const instance = axios.create({
    baseURL: URL,
    headers:{
        Authorization: `Bearer` + "Тут токен",
    }
})