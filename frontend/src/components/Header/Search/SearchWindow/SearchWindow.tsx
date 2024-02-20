import styles from './SearchWindow.module.css';
import SearchWindowBlock from '../SearchWindowBlock/SearchWindowBlock.tsx';
import { ReactNode, useCallback, useEffect, useRef } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from '../../../../store/store.ts';
import { setSearchWindowIsActive } from '../../../../store/slices/windowSearchSlice.ts';

function SearchWindow() {
  const { blocks, searchWindowIsActive } = useSelector(
    (state: RootState) => state.windowSearch,
  );
  console.log(blocks);
  const isLoading = useSelector((state: RootState) => state.loading.isLoading);
  const searchWindowRef = useRef<HTMLDivElement | null>(null);
  const dispatch = useDispatch();

  const handleClick = useCallback(
    (e: MouseEvent) => {
      if (!searchWindowRef.current) return;
      if (!searchWindowRef.current.contains(e.target as Node)) {
        dispatch(setSearchWindowIsActive(false));
      }
    },
    [dispatch],
  );

  useEffect(() => {
    if (!searchWindowIsActive || isLoading) return;
    document.addEventListener('click', handleClick);
    return () => document.removeEventListener('click', handleClick);
  }, [searchWindowIsActive, isLoading, handleClick]);

  const searchWindowBlocks: Array<ReactNode> = blocks.map(searchBlockObject => {
    return (
      <SearchWindowBlock
        img={searchBlockObject.img}
        title={searchBlockObject.title}
        text={searchBlockObject.text}
        isActive={searchBlockObject.isActive}
      />
    );
  });
  return (
    <div
      ref={searchWindowRef}
      className={
        searchWindowIsActive
          ? `${styles.searchWindowActive} ${styles.searchWindow}`
          : styles.searchWindow
      }
    >
      {...searchWindowBlocks}
    </div>
  );
}

export default SearchWindow;
