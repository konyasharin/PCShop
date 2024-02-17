import styles from './Builds.module.css';
import Container from '../Container/Container.tsx';
import mainPrize from '../../assets/mainPrize.png';

import Top from './Top.tsx';
import sysblock from '../../assets/sysblock.png';
import baccom from '../../assets/baccom.png';
import gprize from '../../assets/gprize.png';
import sprize from '../../assets/sprize.png';
import bprize from '../../assets/bprize.png';

function Builds() {
  return (
    <section className={styles.body}>
      <Container className={styles.container}>
        <h2 className={styles.name}>ТОП сборок</h2>
        <img src={mainPrize} alt={'mainPrize'} className={styles.mainPrize} />
        <div className={styles.blocks}>
          <Top
            className={styles.block}
            description={[
              'RTX 4080 16GB',
              'Ryzen 7 4600',
              '32GB RAM',
              'Водяное охлаждение',
            ]}
            sysblockSrc={sysblock}
            prizeSrc={gprize}
            name={'КОЛИБРИ'}
            baccomSrc={baccom}
          />
          <Top
            className={styles.block}
            description={[
              'RTX 4080 16GB',
              'Ryzen 7 4600',
              '32GB RAM',
              'Водяное охлаждение',
            ]}
            sysblockSrc={sysblock}
            prizeSrc={sprize}
            name={'КОЛИБРИ'}
            baccomSrc={baccom}
          />
          <Top
            className={styles.block}
            description={[
              'RTX 4080 16GB',
              'Ryzen 7 4600',
              '32GB RAM',
              'Водяное охлаждение',
            ]}
            sysblockSrc={sysblock}
            prizeSrc={bprize}
            name={'КОЛИБРИ'}
            baccomSrc={baccom}
          />
        </div>
      </Container>
    </section>
  );
}

export default Builds;
