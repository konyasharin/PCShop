import styles from './SearchWindow.module.css';
import SearchWindowBlock from '../SearchWindowBlock/SearchWindowBlock.tsx';
import React, { ReactNode } from 'react';
import { TSearchBlock } from '../../../../hooks/useSearch.ts';

type SearchWindowProps = {
  isActive: boolean;
  blocks: Array<TSearchBlock>;
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
  /*
  useEffect(() => {
    let i = 0;
    const timeInterval = setInterval(() => {
      console.log('123');
      searchWindowBlocks[i] = (
        <SearchWindowBlock
          img={props.blocks[i].img}
          title={props.blocks[i].title}
          text={props.blocks[i].text}
          isActive={true}
        />
      );
      i += 1;
      if (i >= searchWindowBlocks.length) {
        clearInterval(timeInterval);
      }
    }, 500);
  }, [props.isActive]);
*/
  return (
    <div
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
