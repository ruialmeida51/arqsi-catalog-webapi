using System;
using Server.Service.AES;
using Xunit;

namespace Server.UnitTests.ServiceTests
{
    public class AesServiceTest
    {
        [Fact]
        public void EncryptStringToBytes_Aes_NullException_PlainText_Test()
        {
            // encrypt null
            try
            {
                AesService.EncryptStringToBytes_Aes(null);
                Assert.True(false, "Should throw NullException");
            }
            catch (ArgumentNullException)
            {
                Assert.True(true);
            }
            catch (Exception e)
            {
                Assert.True(false, e.ToString());
            }
            
        }
        
        [Fact]
        public void EncryptStringToBytes_Aes_TwoTimesTheSame_Test()
        {
            var str = "Should be encrypted two times the same way";
            try
            {
                var enc1 = AesService.EncryptStringToBytes_Aes(str);
                var enc2 = AesService.EncryptStringToBytes_Aes(str);
                
                Assert.Equal(enc1, enc2);
            }
            catch (Exception e)
            {
                Assert.True(false, e.ToString());
            }
        }
        
        [Fact]
        public void DecryptStringFromBytes_Aes_TwoTimesTheSame_Test()
        {
            var str = "Should be decrypted two times the same way";
            try
            {
                var enc1 = AesService.EncryptStringToBytes_Aes(str);
                var dec1 = AesService.DecryptStringFromBytes_Aes(enc1);
                var dec2 = AesService.DecryptStringFromBytes_Aes(enc1);
                
                Assert.Equal(dec1, dec2);
            }
            catch (Exception e)
            {
                Assert.True(false, e.ToString());
            }
        }
        
        [Fact]
        public void EncryptAndDecrypt_Test()
        {
            var str = "Should be encrypted and decrypted";
            try
            {
                var enc1 = AesService.EncryptStringToBytes_Aes(str);
                var dec1 = AesService.DecryptStringFromBytes_Aes(enc1);
                
                Assert.Equal(dec1, str);
            }
            catch (Exception e)
            {
                Assert.True(false, e.ToString());
            }
        }
    }
}