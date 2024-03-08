import { useState } from 'react';
import styles from './Search.module.css';
import searchImg from '../../../assets/search.svg';
import search from '../../../api/search.ts';
import { useDispatch, useSelector } from 'react-redux';
import { setIsLoading } from 'store/slices/loadingSlice.ts';
import { RootState } from 'store/store.ts';
import {
  setActivateBlock,
  setBlocks,
  setSearchWindowIsActive,
} from 'store/slices/windowSearchSlice.ts';

function Search() {
  const [searchString, setSearchString] = useState('');
  const dispatch = useDispatch();
  const { searchBtnIsActive } = useSelector(
    (state: RootState) => state.windowSearch,
  );

  return (
    <div className={styles.search}>
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
          if (searchBtnIsActive) {
            dispatch(setSearchWindowIsActive(true));
            dispatch(setIsLoading(true));
            search().then(data => {
              console.log('ставим блоки');
              dispatch(setBlocks(data));
              dispatch(setIsLoading(false));
              for (let i = 0; i < data.length; i++) {
                setTimeout(
                  () => {
                    dispatch(setActivateBlock({ index: i, newIsActive: true }));
                  },
                  500 * (i + 1),
                );
              }
            });
          }
        }}
      />
    </div>
  );
}

export default Search;
