import styles from './Builds.module.css';
import Container from '../Container/Container.tsx';
import mainPrize from '../../assets/mainPrize.png';
// eslint-disable-next-line @typescript-eslint/no-unused-vars
import Top1 from './Top1.tsx';
import Top2 from './Top2.tsx';
import Top3 from './Top3.tsx';
function Builds() {
  return (
    <section className={styles.body}>
      <Container className={styles.container}>
        <h2 className={styles.name}>ТОП сборок</h2>
        <img src={mainPrize} alt={'mainPrize'} className={styles.mainPrize} />
        <div className={styles.blocks}>
          <div>
            <Top1 />
          </div>
          <div className={styles.Top2}>
            <Top2 />
          </div>
          <div className={styles.Top3}>
            <Top3 />
          </div>
        </div>
      </Container>
    </section>
  );
}

export default Builds;
