import axios, { AxiosInstance } from 'axios';
import config from '../../config.ts';

const instance: AxiosInstance = axios.create({
  baseURL: config.apiUrl,
});

export default instance;
