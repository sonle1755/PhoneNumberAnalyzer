/* eslint-disable */
/* tslint:disable */
// @ts-nocheck
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

/** @format int32 */
export enum RuleOperator {
  And = 0,
  Or = 1,
}

/** @format int32 */
export enum PatternRuleType {
  EqualsPosition = 1,
  NotEqualsPosition = 2,
  GreaterThanPosition = 3,
  LessThanPosition = 4,
  GreaterThanPrevious = 5,
  LessThanPrevious = 6,
  ValueWhitelist = 7,
  ValueBlacklist = 8,
  ContainsRepeatedSubstring = 9,
}

export interface LoginRequestDto {
  username?: string | null;
  password?: string | null;
}

export interface PatternRuleCreateCommand {
  name?: string | null;
  ruleType?: PatternRuleType;
  /** @format int32 */
  length?: number;
  values?: string[];
  targetPositions?: number[];
  /** @format int32 */
  referencePosition?: number | null;
}

export interface PatternRuleDetail {
  /** @format uuid */
  id?: string;
  name?: string | null;
  ruleType?: PatternRuleType;
  /** @format int32 */
  length?: number;
  values?: string[];
  targetPositions?: number[];
  /** @format int32 */
  referencePosition?: number | null;
}

export interface PatternRuleGroupCreateCommand {
  name?: string | null;
  ruleOperator?: RuleOperator;
  childGroups?: PatternRuleGroupCreateCommand[];
  rules?: PatternRuleCreateCommand[];
}

export interface PatternRuleGroupDetail {
  /** @format uuid */
  id?: string;
  name?: string | null;
  /** @format int32 */
  level?: number;
  isRoot?: boolean;
  ruleOperator?: RuleOperator;
  rules?: PatternRuleDetail[];
  childGroups?: PatternRuleGroupDetail[];
}

export interface PatternTemplateCreateCommand {
  name?: string | null;
  description?: string | null;
  patternRuleGroup?: PatternRuleGroupCreateCommand;
}

export interface PatternTemplateDetail {
  /** @format uuid */
  id?: string;
  name?: string | null;
  description?: string | null;
  patternRuleGroup?: PatternRuleGroupDetail;
}

export interface PatternTemplateMatch {
  /** @format uuid */
  templateId?: string;
  templateName?: string | null;
}

export interface PatternTemplateUpdateCommand {
  name?: string | null;
  description?: string | null;
}

export interface PhoneAnalysisResult {
  originalInput?: string | null;
  normalizedDigits?: string | null;
  matchedPatterns?: PatternTemplateMatch[] | null;
}

export interface RegistrationRequestDto {
  firstName?: string | null;
  lastName?: string | null;
  userName?: string | null;
  password?: string | null;
  avatarUrl?: string | null;
  email?: string | null;
}

export interface UserDto {
  /** @format int32 */
  id?: number;
  firstName?: string | null;
  lastName?: string | null;
  username?: string | null;
  avatarUrl?: string | null;
  emailVerified?: boolean;
  email?: string | null;
  /** @format date-time */
  deletedAt?: string | null;
  /** @format date-time */
  lastLoginAt?: string | null;
}

import type {
  AxiosInstance,
  AxiosRequestConfig,
  AxiosResponse,
  HeadersDefaults,
  ResponseType,
} from "axios";
import axios from "axios";

export type QueryParamsType = Record<string | number, any>;

export interface FullRequestParams
  extends Omit<AxiosRequestConfig, "data" | "params" | "url" | "responseType"> {
  /** set parameter to `true` for call `securityWorker` for this request */
  secure?: boolean;
  /** request path */
  path: string;
  /** content type of request body */
  type?: ContentType;
  /** query params */
  query?: QueryParamsType;
  /** format of response (i.e. response.json() -> format: "json") */
  format?: ResponseType;
  /** request body */
  body?: unknown;
}

export type RequestParams = Omit<
  FullRequestParams,
  "body" | "method" | "query" | "path"
>;

export interface ApiConfig<SecurityDataType = unknown>
  extends Omit<AxiosRequestConfig, "data" | "cancelToken"> {
  securityWorker?: (
    securityData: SecurityDataType | null,
  ) => Promise<AxiosRequestConfig | void> | AxiosRequestConfig | void;
  secure?: boolean;
  format?: ResponseType;
}

export enum ContentType {
  Json = "application/json",
  JsonApi = "application/vnd.api+json",
  FormData = "multipart/form-data",
  UrlEncoded = "application/x-www-form-urlencoded",
  Text = "text/plain",
}

export class HttpClient<SecurityDataType = unknown> {
  public instance: AxiosInstance;
  private securityData: SecurityDataType | null = null;
  private securityWorker?: ApiConfig<SecurityDataType>["securityWorker"];
  private secure?: boolean;
  private format?: ResponseType;

  constructor({
    securityWorker,
    secure,
    format,
    ...axiosConfig
  }: ApiConfig<SecurityDataType> = {}) {
    this.instance = axios.create({
      ...axiosConfig,
      baseURL: axiosConfig.baseURL || "",
    });
    this.secure = secure;
    this.format = format;
    this.securityWorker = securityWorker;
  }

  public setSecurityData = (data: SecurityDataType | null) => {
    this.securityData = data;
  };

  protected mergeRequestParams(
    params1: AxiosRequestConfig,
    params2?: AxiosRequestConfig,
  ): AxiosRequestConfig {
    const method = params1.method || (params2 && params2.method);

    return {
      ...this.instance.defaults,
      ...params1,
      ...(params2 || {}),
      headers: {
        ...((method &&
          this.instance.defaults.headers[
            method.toLowerCase() as keyof HeadersDefaults
          ]) ||
          {}),
        ...(params1.headers || {}),
        ...((params2 && params2.headers) || {}),
      },
    };
  }

  protected stringifyFormItem(formItem: unknown) {
    if (typeof formItem === "object" && formItem !== null) {
      return JSON.stringify(formItem);
    } else {
      return `${formItem}`;
    }
  }

  protected createFormData(input: Record<string, unknown>): FormData {
    if (input instanceof FormData) {
      return input;
    }
    return Object.keys(input || {}).reduce((formData, key) => {
      const property = input[key];
      const propertyContent: any[] =
        property instanceof Array ? property : [property];

      for (const formItem of propertyContent) {
        const isFileType = formItem instanceof Blob || formItem instanceof File;
        formData.append(
          key,
          isFileType ? formItem : this.stringifyFormItem(formItem),
        );
      }

      return formData;
    }, new FormData());
  }

  public request = async <T = any, _E = any>({
    secure,
    path,
    type,
    query,
    format,
    body,
    ...params
  }: FullRequestParams): Promise<AxiosResponse<T>> => {
    const secureParams =
      ((typeof secure === "boolean" ? secure : this.secure) &&
        this.securityWorker &&
        (await this.securityWorker(this.securityData))) ||
      {};
    const requestParams = this.mergeRequestParams(params, secureParams);
    const responseFormat = format || this.format || undefined;

    if (
      type === ContentType.FormData &&
      body &&
      body !== null &&
      typeof body === "object"
    ) {
      body = this.createFormData(body as Record<string, unknown>);
    }

    if (
      type === ContentType.Text &&
      body &&
      body !== null &&
      typeof body !== "string"
    ) {
      body = JSON.stringify(body);
    }

    return this.instance.request({
      ...requestParams,
      headers: {
        ...(requestParams.headers || {}),
        ...(type ? { "Content-Type": type } : {}),
      },
      params: query,
      responseType: responseFormat,
      data: body,
      url: path,
    });
  };
}

/**
 * @title PhoneNumberAnalyzer.Api
 * @version 1.0
 */
export class Api<
  SecurityDataType extends unknown,
> extends HttpClient<SecurityDataType> {
  api = {
    /**
     * No description
     *
     * @tags Auth
     * @name AuthRegisterCreate
     * @request POST:/api/Auth/Register
     */
    authRegisterCreate: (
      data: RegistrationRequestDto,
      params: RequestParams = {},
    ) =>
      this.request<void, any>({
        path: `/api/Auth/Register`,
        method: "POST",
        body: data,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Auth
     * @name AuthLoginCreate
     * @request POST:/api/Auth/Login
     */
    authLoginCreate: (data: LoginRequestDto, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/Auth/Login`,
        method: "POST",
        body: data,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags PatternTemplate
     * @name PatternTemplateList
     * @request GET:/api/PatternTemplate
     */
    patternTemplateList: (params: RequestParams = {}) =>
      this.request<PatternTemplateDetail[], any>({
        path: `/api/PatternTemplate`,
        method: "GET",
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags PatternTemplate
     * @name PatternTemplateCreate
     * @request POST:/api/PatternTemplate
     */
    patternTemplateCreate: (
      data: PatternTemplateCreateCommand,
      params: RequestParams = {},
    ) =>
      this.request<PatternTemplateDetail, any>({
        path: `/api/PatternTemplate`,
        method: "POST",
        body: data,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags PatternTemplate
     * @name PatternTemplateDetail
     * @request GET:/api/PatternTemplate/{id}
     */
    patternTemplateDetail: (id: string, params: RequestParams = {}) =>
      this.request<PatternTemplateDetail, any>({
        path: `/api/PatternTemplate/${id}`,
        method: "GET",
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags PatternTemplate
     * @name PatternTemplateUpdate
     * @request PUT:/api/PatternTemplate/{id}
     */
    patternTemplateUpdate: (
      id: string,
      data: PatternTemplateUpdateCommand,
      params: RequestParams = {},
    ) =>
      this.request<void, any>({
        path: `/api/PatternTemplate/${id}`,
        method: "PUT",
        body: data,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags PatternTemplate
     * @name PatternTemplateDisableUpdate
     * @request PUT:/api/PatternTemplate/Disable/{id}
     */
    patternTemplateDisableUpdate: (id: string, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/PatternTemplate/Disable/${id}`,
        method: "PUT",
        ...params,
      }),

    /**
     * No description
     *
     * @tags PatternTemplate
     * @name PatternTemplateEnableUpdate
     * @request PUT:/api/PatternTemplate/Enable/{id}
     */
    patternTemplateEnableUpdate: (id: string, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/PatternTemplate/Enable/${id}`,
        method: "PUT",
        ...params,
      }),

    /**
     * No description
     *
     * @tags PhoneAnalysis
     * @name PhoneAnalysisCreate
     * @request POST:/api/PhoneAnalysis
     */
    phoneAnalysisCreate: (data: string, params: RequestParams = {}) =>
      this.request<PhoneAnalysisResult[], any>({
        path: `/api/PhoneAnalysis`,
        method: "POST",
        body: data,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags User
     * @name UserList
     * @request GET:/api/User
     */
    userList: (params: RequestParams = {}) =>
      this.request<UserDto[], any>({
        path: `/api/User`,
        method: "GET",
        format: "json",
        ...params,
      }),
  };
}
