import styles from './TOPBuilds.module.css';
import TOPBuildsBlock from './TOPBuildsBlock/TOPBuildsBlock.tsx';
import PCImg from 'assets/sysblock.png';
import goldPrizeImg from 'assets/gprize.png';
import silverPrizeImg from 'assets/sprize.png';
import bronzePrizeImg from 'assets/bprize.png';
import mainPrize from 'assets/mainPrize.png';
import Container from 'components/Container/Container.tsx';

function TOPBuilds() {
  return (
    <section className={styles.body}>
      <Container className={styles.container}>
        <h2 className={styles.name}>ТОП сборок</h2>
        <img src={mainPrize} alt={'mainPrize'} className={styles.mainPrize} />
        <div className={styles.blocks}>
          <TOPBuildsBlock
            className={styles.block}
            description={[
              'RTX 4080 16GB',
              'Ryzen 7 4600',
              '32GB RAM',
              'Водяное охлаждение',
            ]}
            PCImg={PCImg}
            prizeImg={goldPrizeImg}
            name={'КОЛИБРИ'}
          />
          <TOPBuildsBlock
            className={styles.block}
            description={[
              'RTX 4080 16GB',
              'Ryzen 7 4600',
              '32GB RAM',
              'Водяное охлаждение',
            ]}
            PCImg={PCImg}
            prizeImg={silverPrizeImg}
            name={'КОЛИБРИ'}
          />
          <TOPBuildsBlock
            className={styles.block}
            description={[
              'RTX 4080 16GB',
              'Ryzen 7 4600',
              '32GB RAM',
              'Водяное охлаждение',
            ]}
            PCImg={PCImg}
            prizeImg={bronzePrizeImg}
            name={'КОЛИБРИ'}
          />
        </div>
      </Container>
    </section>
  );
}

export default TOPBuilds;
