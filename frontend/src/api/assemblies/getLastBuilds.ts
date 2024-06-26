import { AxiosResponse } from 'axios';
import TAssembly from 'types/TAssembly.ts';
import axios from 'api/axios.ts';
import config from '../../../config.ts';

/**
 * Получает последние сборки
 * @param offset Количество сборок которые нужно пропустить (пропуск от начала списка последних созданных сборок)
 */
function getLastBuilds(
  offset: number,
): Promise<AxiosResponse<{ assemblies: TAssembly<string>[] }>> {
  return axios.get(`${config.apiUrl}/assembly/getLast?offset=${offset}`);
}

export default getLastBuilds;
