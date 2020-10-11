using System.Threading.Tasks;

namespace TinyBeans.Logging.Tests.Dummy {
    internal class DummyClass {
        public void Method() {
        }

        public void Method(DummyPoco dummyPoco) {
        }

        public void Method(DummyPoco dummyPoco1, DummyPoco dummyPoco2) {
        }

        public void Method(DummyPoco dummyPoco1, DummyPoco dummyPoco2, DummyPoco dummyPoco3) {
        }

        public void Method(DummyPoco dummyPoco1, DummyPoco dummyPoco2, DummyPoco dummyPoco3, DummyPoco dummyPoco4) {
        }

        public void Method(DummyPoco dummyPoco1, DummyPoco dummyPoco2, DummyPoco dummyPoco3, DummyPoco dummyPoco4, DummyPoco dummyPoco5) {
        }

        public DummyPoco ResultMethod() {
            return new DummyPoco();
        }

        public DummyPoco ResultMethod(DummyPoco dummyPoco) {
            return new DummyPoco();
        }

        public DummyPoco ResultMethod(DummyPoco dummyPoco1, DummyPoco dummyPoco2) {
            return new DummyPoco();
        }

        public DummyPoco ResultMethod(DummyPoco dummyPoco1, DummyPoco dummyPoco2, DummyPoco dummyPoco3) {
            return new DummyPoco();
        }

        public DummyPoco ResultMethod(DummyPoco dummyPoco1, DummyPoco dummyPoco2, DummyPoco dummyPoco3, DummyPoco dummyPoco4) {
            return new DummyPoco();
        }

        public DummyPoco ResultMethod(DummyPoco dummyPoco1, DummyPoco dummyPoco2, DummyPoco dummyPoco3, DummyPoco dummyPoco4, DummyPoco dummyPoco5) {
            return new DummyPoco();
        }

        public async Task MethodAsync() {
            await Task.CompletedTask;
        }

        public async Task MethodAsync(DummyPoco dummyPoco) {
            await Task.CompletedTask;
        }

        public async Task MethodAsync(DummyPoco dummyPoco1, DummyPoco dummyPoco2) {
            await Task.CompletedTask;
        }

        public async Task MethodAsync(DummyPoco dummyPoco1, DummyPoco dummyPoco2, DummyPoco dummyPoco3) {
            await Task.CompletedTask;
        }

        public async Task MethodAsync(DummyPoco dummyPoco1, DummyPoco dummyPoco2, DummyPoco dummyPoco3, DummyPoco dummyPoco4) {
            await Task.CompletedTask;
        }

        public async Task MethodAsync(DummyPoco dummyPoco1, DummyPoco dummyPoco2, DummyPoco dummyPoco3, DummyPoco dummyPoco4, DummyPoco dummyPoco5) {
            await Task.CompletedTask;
        }

        public async Task<DummyPoco> ResultMethodAsync() {
            return await Task.FromResult(new DummyPoco());
        }

        public async Task<DummyPoco> ResultMethodAsync(DummyPoco dummyPoco) {
            return await Task.FromResult(new DummyPoco());
        }

        public async Task<DummyPoco> ResultMethodAsync(DummyPoco dummyPoco1, DummyPoco dummyPoco2) {
            return await Task.FromResult(new DummyPoco());
        }

        public async Task<DummyPoco> ResultMethodAsync(DummyPoco dummyPoco1, DummyPoco dummyPoco2, DummyPoco dummyPoco3) {
            return await Task.FromResult(new DummyPoco());
        }

        public async Task<DummyPoco> ResultMethodAsync(DummyPoco dummyPoco1, DummyPoco dummyPoco2, DummyPoco dummyPoco3, DummyPoco dummyPoco4) {
            return await Task.FromResult(new DummyPoco());
        }

        public async Task<DummyPoco> ResultMethodAsync(DummyPoco dummyPoco1, DummyPoco dummyPoco2, DummyPoco dummyPoco3, DummyPoco dummyPoco4, DummyPoco dummyPoco5) {
            return await Task.FromResult(new DummyPoco());
        }
    }
}