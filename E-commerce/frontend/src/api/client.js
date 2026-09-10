import axios from 'axios'
const api=axios.create({baseURL:import.meta.env.VITE_API_URL||'https://localhost:7001/api'})
api.interceptors.request.use(c=>{const t=localStorage.getItem('electromart_token');if(t)c.headers.Authorization=`Bearer ${t}`;return c})
export default api
