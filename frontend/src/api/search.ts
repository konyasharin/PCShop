import img from '../assets/videocard.jpg';
import { TSearchBlock } from '../hooks/useWindowSearch.ts';

const blocksData: Array<TSearchBlock> = [
  {
    img: img,
    title: 'Gigabyte RTX 3080',
    text: '16GB, 128 бит, GDDR6, HDMI, DVI-D',
    isActive: false,
  },
  {
    img: img,
    title: 'Gigabyte RTX 3080',
    text: '16GB, 128 бит, GDDR6, HDMI, DVI-D',
    isActive: false,
  },
  {
    img: img,
    title: 'Gigabyte RTX 3080',
    text: '16GB, 128 бит, GDDR6, HDMI, DVI-D',
    isActive: false,
  },
];
async function search() {
  // тут будет запрос к api
  return new Promise<TSearchBlock[]>(resolve => {
    setTimeout(() => {
      console.log('Пришел ответ');
      const blocks: TSearchBlock[] = [];
      blocksData.forEach(block => {
        blocks.push({ ...block });
      });
      resolve([...blocks]);
    }, 1000);
  });
}

export default search;
