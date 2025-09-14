import { getServerToken } from "@/servers/tokens";
import axios from "axios";

const axiosInstance = axios.create({
  baseURL: process.env.CLOUD_API_SERVER_URL!,
  timeout: 10000,
});

axiosInstance.interceptors.request.use(
  async function (config) {
    const token = await getServerToken();
    if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
  },
  function (error) {
    console.error(error);
    return Promise.reject(error);
  }
);

export default axiosInstance;
