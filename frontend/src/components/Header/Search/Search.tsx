import React, { useState } from 'react';
import styles from './Search.module.css';
import searchImg from '../../../assets/search.svg';
import search from '../../../api/search.ts';
import { useDispatch } from 'react-redux';
import { setIsLoading } from '../../../store/slices/loadingSlice.ts';
import { TSearchBlock } from '../../../hooks/useWindowSearch.ts';

type SearchProps = {
  setIsActive: (newIsActive: boolean) => void;
  searchRef: React.MutableRefObject<HTMLDivElement | null>;
  searchBtnIsActive: boolean;
  setBlocks: (blocks: TSearchBlock[]) => void;
};

const testState: { data: TSearchBlock[] } = {
  data: [],
};

const Search: React.FC<SearchProps> = props => {
  const [searchString, setSearchString] = useState('');
  const dispatch = useDispatch();

  return (
    <div className={styles.search} ref={props.searchRef}>
      <input
        onChange={e => setSearchString(e.target.value)}
        value={searchString}
        placeholder={'Поиск'}
        className={styles.searchInput}
      />
      <img
        src={searchImg}
        alt="search"
        className={styles.searchBtn}
        onClick={() => {
          if (props.searchBtnIsActive) {
            props.setIsActive(true);
            dispatch(setIsLoading(true));
            search().then(data => {
              console.log('ставим блоки');
              testState.data = data;
              props.setBlocks(testState.data);
              dispatch(setIsLoading(false));
            });
          }
        }}
      />
    </div>
  );
};

export default Search;
