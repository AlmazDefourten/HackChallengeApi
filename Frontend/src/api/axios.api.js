import axios from "axios";

// const URL = ''

// export const instance = axios.create({
//     baseURL: URL,
//     headers:{
//         Authorization: `Bearer` + "Тут токен",
//     }
// })


export const API_URL = "http://31.129.105.161";

const $api = axios.create({
    withCredentials: true,
    baseURL: API_URL,
})

$api.interceptors.request.use((config) => {
    config.headers.Authorization = `Bearer ${localStorage.getItem('refreshToken')}`
    return config;
})

$api.interceptors.response.use((config) =>{
    return config;
}, (error) =>{
    const originalRequest = error.config
    // console.log(originalRequest)
    if (error.response.status === 400 && error.config){
        console.log("НЕВЕРНЫЕ ДАННЫЕ")
        return
    }

    if (error.response.status === 401 && error.config && !originalRequest._isRetry){
        originalRequest._isRetry = true
        try{
            axios({
                method:'get',
                url: `${API_URL}/refresh`,
                withCredentials: true
             })
             .then((result)=>{
                const {data} = result;
                localStorage.setItem('token', data.access_token)
             })
             return $api.request(originalRequest)
        } catch(e) {
            console.log("ВЫ НЕ АВТОРИЗОВАНЫ")
        }
    }
    throw error;
})

export default $api;