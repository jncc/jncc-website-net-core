
var civicCookieControlConfig = {
    apiKey: 'fc8364648ad258321fa6197d91d72771562de378',
    product: 'PRO',
    logConsent: true,
    initialState: 'notify',
    notifyDismissButton: false,
    rejectButton: false,
    setInnerHTML: true,
    closeStyle: 'button',
    text: {
        notifyTitle: '<span class="civic-cookie-banner-title">We use cookies</span>',
        notifyDescription: 'Some of these cookies are essential, while others help us to improve your experience by providing insights into how the site is being used.'
            + ' For more detailed information, please read our <a href="https://jncc.gov.uk/about-jncc/corporate-information/cookie-policy/" class="civic-cookie-control-link">cookie notice</a>.',
        accept: 'Accept all cookies',
        reject: 'Customise settings',
        title: 'We use cookies',
        settings: 'Customise settings',
        intro: '<p>Some of these cookies are essential, while others help us to improve your experience by providing insights into how the site is being used.</p>'
            + '<p>For more detailed information, please read our <a href="https://jncc.gov.uk/about-jncc/corporate-information/cookie-policy/" class="civic-cookie-control-link">cookie notice</a>.</p>',
        necessaryTitle: 'Essential cookies',
        necessaryDescription: 'Essential cookies enable core functionality. The site cannot function without these cookies and can only be disabled by changing your browser preferences.',
        closeLabel: 'Save settings'  // closing doesn't actually *set* anything - settings already saved on toggle!
    },
    branding: {
        removeAbout: true,
        backgroundColor: '#222',
        toggleColor: '#3f9c35',
        fontFamily: '"Roboto", "Helvetica Neue", Helvetica, Arial, sans-serif',
        fontSize: '1em',
        fontSizeTitle: '2em'
    },

    optionalCookies: [
        {
            name: 'analytics',
                    recommendedState: false,
                    label: 'Analytics cookies',
            description: 'Wed like to set Google Analytics cookies to help us improve our website by collecting and reporting information on how you use it. The cookies collect information in a way that does not directly identify anyone. For more information on how these cookies work please see our Cookies page.',
                    cookies: ['_gat_UA-15841534-6', '_gid', '_ga'],
                    onAccept: function () {

                        dataLayer.push({ 'event': 'analytics_consent_accepted' });
                    },
                    onRevoke: function () {

                        dataLayer.push({ 'event': 'analytics_consent_withdrawn' });
                        window['ga-disable-UA-15841534-6'] = true;

                    }
        },
        {
            name: 'youtube',
            label: 'Youtube Cookies',
            description: 'We embed videos from our official YouTube channel using YouTube’s privacy-enhanced mode. This mode may set cookies on your computer once you click on the YouTube video player, but YouTube will not store personally-identifiable cookie information for playbacks of embedded videos using the privacy-enhanced mode.',
            cookies: ['PREF*','VSC*','VISITOR_INFO1_LIVE*','remote_sid*'],
            onAccept: function () {
              
            },
            onRevoke: function () {
                eraseCookie('PREF*');
                eraseCookie('VSC*');
                eraseCookie('VISITOR_INFO1_LIVE*');
                eraseCookie('remote_sid*');
            }
        },
        {
            name: 'twitter',
            label: 'Twitter Cookies',
            description: 'We use twitter plugins, to allow you to share certain pages of our website on social media',
            cookies: ['guest_id','lang','personalization_id','_twitter_sess'],
            onAccept: function () {

            },
            onRevoke: function () {
                eraseCookie('guest_id');
                eraseCookie('lang');
                eraseCookie('personalization_id');
                eraseCookie('_twitter_sess');
            }
        }
    ]
};

CookieControl.load(civicCookieControlConfig);