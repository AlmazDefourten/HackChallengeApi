import styles from './header.module.css';

function Header() {
    return ( 
    <header className='font-bahnschrift font-size-25px font-color-header font-weight-600' style={{width:'100vw', height:'8.5vh'}}>
        <div className={styles.login_button_and_title_and_home_links}>
            <div className={styles.title_and_home_links}>
                <div className={styles.title}>
                    <span>SO.</span>
                    <span>MNOY</span>
                </div>
                <div className={styles.home_links}>
                    <span>Home</span>
                    <span>About us</span>
                    <span>Contacts</span>
                </div>
            </div>
            <span>Login</span>
        </div>
    </header>
    );
}

export default Header;