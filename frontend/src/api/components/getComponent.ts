import axios from 'api/axios.ts';
import config from '../../../config.ts';
import { AxiosResponse } from 'axios';
import componentTypes from 'enums/componentTypes.ts';
import TVideoCard from 'types/components/TVideoCard.ts';
import TProcessor from 'types/components/TProcessor.ts';

async function getComponent(
  componentType: keyof typeof componentTypes,
  id: number,
): Promise<AxiosResponse<TVideoCard<string> | TProcessor<string>>> {
  let url: string;
  switch (componentType) {
    case 'videoCard':
      url = '/VideoCard/getVideoCard';
      break;
    case 'processor':
      url = '/Processor/getProcessor';
      break;
    case 'motherBoard':
      url = '/MotherBoard/getMotherBoard';
      break;
  }
  return await axios.get(config.apiUrl + url + `/${id}`);
}

export default getComponent;
