import styles from './SearchWindow.module.css';
import SearchWindowBlock from '../SearchWindowBlock/SearchWindowBlock.tsx';
import React, { ReactNode } from 'react';
import { TSearchBlock } from '../../../../hooks/useWindowSearch.ts';

type SearchWindowProps = {
  isActive: boolean;
  blocks: Array<TSearchBlock>;
  searchWindowRef: React.MutableRefObject<HTMLDivElement | null>;
};

const SearchWindow: React.FC<SearchWindowProps> = props => {
  const searchWindowBlocks: Array<ReactNode> = props.blocks.map(
    searchBlockObject => {
      return (
        <SearchWindowBlock
          img={searchBlockObject.img}
          title={searchBlockObject.title}
          text={searchBlockObject.text}
          isActive={searchBlockObject.isActive}
        />
      );
    },
  );
  return (
    <div
      ref={props.searchWindowRef}
      className={
        props.isActive
          ? `${styles.searchWindowActive} ${styles.searchWindow}`
          : styles.searchWindow
      }
    >
      {...searchWindowBlocks}
    </div>
  );
};

export default SearchWindow;
