import Header from '../../components/Header/Header.tsx';
import styles from './MainPage.module.css';

function MainPage() {
  return (
    <>
      <div className={styles.ellipses}>
        <div className={styles.ellipse} id={'ellipse1'}></div>
        <div className={styles.ellipse} id={'ellipse2'}></div>
      </div>
      <Header />
    </>
  );
}

export default MainPage;
