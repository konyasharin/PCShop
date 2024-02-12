import React, { useState } from 'react';
import styles from './Search.module.css';
import searchImg from '../../../assets/search.svg';

type SearchProps = {
  setIsActive: (newIsActive: boolean) => void;
  searchRef: React.MutableRefObject<HTMLDivElement | null>;
};

const Search: React.FC<SearchProps> = props => {
  const [searchString, setSearchString] = useState('');

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
        onClick={() => props.setIsActive(true)}
      />
    </div>
  );
};

export default Search;
