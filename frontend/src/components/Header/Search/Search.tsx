import { useState } from 'react';
import styles from './Search.module.css';
import searchImg from '../../../assets/search.svg';

function Search() {
  const [searchString, setSearchString] = useState('');

  return (
    <div className={styles.search}>
      <input
        onChange={e => setSearchString(e.target.value)}
        value={searchString}
        placeholder={'Поиск'}
        className={styles.searchInput}
      />
      <img src={searchImg} alt="search" className={styles.searchBtn} />
    </div>
  );
}

export default Search;
