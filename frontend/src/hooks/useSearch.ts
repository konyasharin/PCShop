import { useState } from 'react';
import img from '../assets/videocard.jpg';

export type TSearchBlock = {
  img: string;
  title: string;
  text: string;
  isActive: boolean;
};

const blocksData = [
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

function useSearch() {
  const [isActive, setIsActive] = useState(false);
  const [blocks, setBlocks] = useState(blocksData);
  function setIsActiveHandle(newIsActive: boolean) {
    setIsActive(newIsActive);
    if (!newIsActive) return;
    let i = 0;
    const timeInterval = setInterval(() => {
      const newBlocks: Array<TSearchBlock> = [];
      blocks.forEach((block, j) => {
        if (j === i) {
          block.isActive = true;
        }
        newBlocks.push(block);
      });
      i++;
      setBlocks(newBlocks);
      if (i >= blocks.length) {
        clearInterval(timeInterval);
      }
    }, 500);
  }
  // Тут будет запрос к api
  return { isActive, setIsActiveHandle, blocks, setBlocks };
}

export default useSearch;
