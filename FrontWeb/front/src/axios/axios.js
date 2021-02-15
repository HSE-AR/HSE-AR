import axios from "axios";

axios.defaults.baseURL = 'https://localhost:5555/wapi/'
axios.defaults.headers.common['Authorization'] =  'Bearer ' + localStorage.getItem('token')