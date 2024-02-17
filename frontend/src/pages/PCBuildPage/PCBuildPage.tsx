import Container from '../../components/Container/Container.tsx';
import styles from './PCBuildPage.module.css';

function PCBuildPage() {
  const scaleInner = 50;

  return (
    <Container>
      <div className={styles.scale}>
        <div
          className={styles.scaleInner}
          style={{ width: `calc(${scaleInner}% + 6px)` }}
        ></div>
      </div>
    </Container>
  );
}

export default PCBuildPage;
