using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class admob : MonoBehaviour
    {
        public InterstitialAd interstitial;
        public RewardedAd _RewardedAd;
        //GEÇİŞ REKLAMI
        public void RequestInterstitial()
        {
            string AdUnitId;
                    #if UNITY_ANDROID
                                AdUnitId = "ca-app-pub-3940256099942544/1033173712";//Example ID
                    #elif UNITY_IPHONE
                                AdUnitId = "ca-app-pub-3940256099942544/4411468910";//Example ID
                    #else 
                            AdUnitId = "unexpected_platform";
                    #endif   
            
            interstitial = new InterstitialAd(AdUnitId);
            AdRequest request = new AdRequest.Builder().Build();// Reklam isteği yollama ve çalıştırma
            interstitial.LoadAd(request); //oluşturulan isteği Loadladık
            interstitial.OnAdClosed +=GecisReklamiKapatildi; //Bu işlem tetiklendiğinde += Geçiş reklamı kapatılırsa sağdaki fonksyion çalışır
        }

        void GecisReklamiKapatildi(object Sender ,EventArgs args)//NORMAL FONKSİYON DEĞİL ABONE OLAN BİR FONKSYİON (PARANTEZ O YÜZDEN BÖYLE DOLU)
        {
            interstitial.Destroy();
            RequestInterstitial();
        }

        public void GecisReklamiGoster()
        {
            if(PlayerPrefs.GetInt("Gecisreklamsayisi")== 2)
            {
                if(interstitial.IsLoaded())
                {
                    PlayerPrefs.SetInt("Gecisreklamsayisi",1);
                    interstitial.Show();
                }
                else
                {
                    interstitial.Destroy();
                    RequestInterstitial();
                }                
            }
            else
            {
                PlayerPrefs.SetInt("Gecisreklamsayisi", PlayerPrefs.GetInt("Gecisreklamsayisi") + 1);
            }
        }

        //ÖDÜLLÜ REKLAM

        public void RequestRewardedAd()
        {
            string AdUnitId;
                    #if UNITY_ANDROID
                                AdUnitId = "ca-app-pub-3940256099942544/5224354917";//Example
                    #elif UNITY_IPHONE
                                AdUnitId = "ca-app-pub-3940256099942544/1712485313";//Example
                    #else 
                                AdUnitId = "unexpected_platform";
                    #endif
            _RewardedAd = new RewardedAd(AdUnitId);
            AdRequest request = new AdRequest.Builder().Build();
            _RewardedAd.LoadAd(request);

            _RewardedAd.OnUserEarnedReward += OdulReklamiTamamlandi;
            _RewardedAd.OnAdClosed += OdulReklamiKapatildi;
            _RewardedAd.OnAdClosed += OdulReklamiYuklendi;
        }
        
        void OdulReklamiTamamlandi(object Sender ,Reward e)
        {
            string type = e.Type;
            double amount = e.Amount;
            Debug.Log("Ödül Alınsın : " + type + "--" + amount);
        }
        
        void OdulReklamiKapatildi(object Sender ,EventArgs e)
        {
            Debug.Log("Ödüllü Reklam Kapatıldı");
            RequestRewardedAd();
        }
        
        void OdulReklamiYuklendi(object Sender ,EventArgs e)
        {
            Debug.Log("Ödüllü Reklam Yüklendi");
            RequestRewardedAd();
        }

        public void OdulReklamiGoster()
        {            
                if(_RewardedAd.IsLoaded())
                {                 
                    _RewardedAd.Show();
                }
        }
    }
