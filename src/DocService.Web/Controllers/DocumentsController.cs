namespace DocService.Web.Controllers
{
    using DocService.Application.Documents.Commands;
    using DocService.Web.Models;
    using FluentValidation;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    using static DocService.Web.Common.Constants;

    public class DocumentsController : BaseController
    {
        private readonly IMediator mediator;

        public DocumentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IActionResult ConvertXmlDocumentToJson()
            => this.View();


        [HttpPost]
        public async Task<IActionResult> ConvertXmlDocumentToJson(ConvertXmlDocumentToJson model)
        {
            var file = model.File;

            var command = new ConvertXmlDocumentToJsonCommand(model.Filename, file.ContentType, file.OpenReadStream());

            try
            {
                await this.mediator.Send(command);

                this.ViewData[ConvertDocumentResultKey] = string.Format(ConvertDocumentResultMessage, model.Filename);
            }
            catch (ValidationException valEx)
            {
                this.ViewData[ConvertDocumentResultKey] = valEx.Message;
            }

            return this.View();
        }
    }
}
