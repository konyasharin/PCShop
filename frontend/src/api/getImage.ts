import axios from 'api/axios.ts';
import config from '../../config.ts';
import { AxiosResponse } from 'axios';

/**
 * Получает из бекапа blob версию картинки
 * @param imageName имя картинки в бекапе
 */
function getImage(imageName: string): Promise<AxiosResponse<{ file: Blob }>> {
  return axios.get(`${config.apiUrl}/backup/getImage?imageName=${imageName}`);
}

export default getImage;
