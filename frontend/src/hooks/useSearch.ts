import { useEffect, useRef, useState } from 'react';
import img from '../assets/videocard.jpg';

export type TSearchBlock = {
  img: string;
  title: string;
  text: string;
  isActive: boolean;
};

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

function useSearch() {
  const [isActive, setIsActive] = useState(false);
  const [blocks, setBlocks] = useState(blocksData);
  function setIsActiveHandle(newIsActive: boolean) {
    setIsActive(newIsActive);
    if (!newIsActive) {
      const timeInterval = setInterval(() => {
        const newBlocks: Array<TSearchBlock> = [];
        blocks.forEach(block => {
          const newBlock = { ...block };
          newBlock.isActive = false;
          newBlocks.push(newBlock);
        });
        setBlocks(newBlocks);
        clearInterval(timeInterval);
      }, 450);
    } else {
      let i = 0;
      const timeInterval = setInterval(() => {
        const newBlocks: Array<TSearchBlock> = [];
        blocks.forEach((block, j) => {
          const newBlock: TSearchBlock = { ...block };
          if (j <= i) {
            newBlock.isActive = true;
          }
          newBlocks.push(newBlock);
        });
        i++;
        setBlocks(newBlocks);
        if (i >= blocks.length) {
          clearInterval(timeInterval);
        }
      }, 500);
    }
  }
  function handleClick(e: MouseEvent) {
    if (!searchWindowRef.current || !searchRef.current) return;
    if (
      !searchWindowRef.current.contains(e.target as Node) &&
      !searchRef.current.contains(e.target as Node)
    ) {
      setIsActiveHandle(false);
    }
  }
  const searchWindowRef = useRef<HTMLDivElement | null>(null);
  const searchRef = useRef<HTMLDivElement | null>(null);
  useEffect(() => {
    if (!isActive) return;
    document.addEventListener('click', handleClick);
    return () => {
      document.removeEventListener('click', handleClick);
    };
  }, [isActive]);
  // Тут будет запрос к api
  return {
    isActive,
    setIsActiveHandle,
    blocks,
    setBlocks,
    searchWindowRef,
    searchRef,
  };
}

export default useSearch;
