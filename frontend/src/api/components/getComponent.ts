import axios from 'api/axios.ts';
import config from '../../../config.ts';
import { AxiosResponse } from 'axios';
import componentTypes from 'enums/componentTypes.ts';
import TOneOfComponents from 'types/components/TOneOfComponents.ts';

/**
 * Получает компонент
 * @param componentType тип компонента
 * @param id id компонента в списке продуктов
 */
async function getComponent(
  componentType: keyof typeof componentTypes,
  id: number,
): Promise<AxiosResponse<TOneOfComponents<string>>> {
  return await axios.get(`${config.apiUrl}/${componentType}/get/${id}`);
}

export default getComponent;
