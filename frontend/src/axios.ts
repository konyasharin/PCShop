// eslint-disable-next-line @typescript-eslint/ban-ts-comment
// @ts-expect-error
import axios, { AxiosInstance } from 'axios';

const instance: AxiosInstance = axios.create({
  baseURL: 'https://localhost:7202/api',
});

export default instance;
