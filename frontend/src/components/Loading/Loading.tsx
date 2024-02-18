import loadingGif from '../../assets/loading.gif';
import { useSelector } from 'react-redux';
import { RootState } from '../../store/store.ts';
import styles from './Loading.module.css';

function Loading() {
  const isLoading = useSelector((state: RootState) => state.loading.isLoading);
  return (
    <div className={isLoading ? styles.loading : styles.loadingDisable}>
      <img src={loadingGif} alt="loading" />
    </div>
  );
}

export default Loading;
