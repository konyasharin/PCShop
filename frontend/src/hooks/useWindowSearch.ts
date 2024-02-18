import React, { useEffect, useRef, useState } from 'react';
import { useSelector } from 'react-redux';
import { RootState } from '../store/store.ts';

export type TSearchBlock = {
  img: string;
  title: string;
  text: string;
  isActive: boolean;
};

function deactivateBlocks(
  setBlocks: (setBlocks: TSearchBlock[]) => void,
  setSearchBtnIsActive: React.Dispatch<React.SetStateAction<boolean>>,
) {
  setBlocks([]);
  setSearchBtnIsActive(true);
}

function activateBlock(
  blocks: Array<TSearchBlock>,
  setBlocks: (setBlocks: TSearchBlock[]) => void,
  index: number,
) {
  const newBlocks = [...blocks];
  newBlocks[index].isActive = true;
  setBlocks(newBlocks);
}

function activateBlocks(
  blocks: TSearchBlock[],
  setBlocks: (setBlocks: TSearchBlock[]) => void,
  animIsPlayRef: React.MutableRefObject<boolean>,
) {
  for (let i = 0; i < blocks.length; i++) {
    setTimeout(
      () => {
        activateBlock(blocks, setBlocks, i);
      },
      500 * (i + 1),
    );
    setTimeout(() => {
      animIsPlayRef.current = false;
    }, 500 * blocks.length);
  }
}

function useWindowSearch() {
  const [searchWindowIsActive, setSearchWindowIsActive] = useState(false);
  const [blocks, setBlocks] = useState<TSearchBlock[]>([]);
  const [searchBtnIsActive, setSearchBtnIsActive] = useState(true);
  const isLoading = useSelector((state: RootState) => state.loading.isLoading);
  const animIsPlayRef = useRef<boolean>(false);
  function setIsActiveHandle(newSearchWindowIsActive: boolean) {
    setSearchWindowIsActive(newSearchWindowIsActive);
    if (!newSearchWindowIsActive) {
      setTimeout(() => deactivateBlocks(setBlocks, setSearchBtnIsActive), 450);
    } else {
      setSearchBtnIsActive(false);
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
    console.log(searchWindowIsActive, isLoading, animIsPlayRef);
    if (!searchWindowIsActive || isLoading || animIsPlayRef.current) return;
    document.addEventListener('click', handleClick);
    return () => {
      document.removeEventListener('click', handleClick);
    };
  }, [animIsPlayRef.current, searchWindowIsActive]);
  useEffect(() => {
    let flag = true;
    for (let i = 0; i < blocks.length; i++) {
      if (blocks[i].isActive) {
        flag = false;
      }
    }
    if (
      flag &&
      !isLoading &&
      searchWindowIsActive &&
      !animIsPlayRef.current &&
      blocks.length
    ) {
      animIsPlayRef.current = true;
      activateBlocks(blocks, setBlocks, animIsPlayRef);
    }
  }, [blocks, isLoading, searchWindowIsActive]);
  return {
    searchWindowIsActive,
    setIsActiveHandle,
    blocks,
    setBlocks,
    searchWindowRef,
    searchRef,
    searchBtnIsActive,
  };
}

export default useWindowSearch;
